import os
import sys
import json
from github import Github
from google import genai
from google.genai import types
import time

def main():
    # Read environment variables injected by GitHub Actions
    github_token = os.getenv("INPUT_GITHUB_TOKEN")
    gemini_api_key = os.getenv("INPUT_GEMINI_API_KEY")
    github_repository = os.getenv("GITHUB_REPOSITORY")
    github_event_path = os.getenv("GITHUB_EVENT_PATH")
    
    if not github_token or not gemini_api_key:
        print("Missing required inputs: github_token or gemini_api_key")
        sys.exit(1)
        
    if not github_event_path or not os.path.exists(github_event_path):
        print("GITHUB_EVENT_PATH is missing or invalid.")
        sys.exit(1)
        
    with open(github_event_path, "r") as f:
        event_data = json.load(f)
        
    if "pull_request" not in event_data:
        print("This action only runs on pull request events.")
        sys.exit(0)
        
    pr_number = event_data["pull_request"]["number"]
    
    # Initialize GitHub client
    print(f"Connecting to GitHub Repo: {github_repository}, PR: {pr_number}")
    g = Github(github_token)
    repo = g.get_repo(github_repository)
    pr = repo.get_pull(pr_number)
    
    # Get PR diff
    print("Fetching PR diff...")
    diff = pr.get_files()
    diff_text = ""
    for file in diff:
        # Ignore deleted files
        if file.status == "removed":
            continue
        diff_text += f"File: {file.filename}\n"
        if file.patch:
            diff_text += f"Patch:\n{file.patch}\n\n"
        
    if not diff_text.strip():
        print("No code changes found in the PR.")
        sys.exit(0)
        
    print("Initializing Gemini Client...")
    client = genai.Client(api_key=gemini_api_key)
    
    prompt = f"""
You are an expert C# code reviewer. Your job is to analyze the provided git diff (code changes) very carefully. 
Check for the following 3 specific areas:
1. SOLID Principles: Are there any violations?
2. Null-Handling: Is there any risk of NullReferenceException?
3. Async/Await: Is asynchronous code written correctly without blocking threads?

Format your response using clear bullet points and a summary table so developers can understand easily.

Here is the git diff:
```diff
{diff_text}
```
"""

    print("Sending diff to Gemini for review...")
    max_retries = 3
    retry_delay = 10
    
    for attempt in range(max_retries):
        try:
            response = client.models.generate_content(
                model='gemini-2.5-flash',
                contents=prompt,
            )
            review_comment = response.text
            break # Success, break out of the retry loop
        except Exception as e:
            print(f"Attempt {attempt + 1} failed: {e}")
            if attempt < max_retries - 1:
                print(f"Retrying in {retry_delay} seconds...")
                time.sleep(retry_delay)
            else:
                print(f"Failed to get response from Gemini after {max_retries} attempts.")
                import traceback
                traceback.print_exc()
                sys.exit(1)
    
    print("Posting review comment to PR...")
    try:
        pr.create_issue_comment(f"### AI Code Review (Gemini)\n\n{review_comment}")
        print("Successfully posted the review!")
    except Exception as e:
        print(f"Failed to post comment to PR: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()
