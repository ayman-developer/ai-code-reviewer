# AI Code Reviewer GitHub Action

## Architecture Overview

The AI Code Reviewer is an automated, AI-powered GitHub Action designed to instantly analyze C# pull requests. The system intercepts code diffs when a Pull Request is opened, specifically targets modified `.cs` files, and uses the Google Gemini 2.5 Flash API to review the code. It then posts inline, line-by-line feedback directly onto the PR diff.

### Architecture Diagram
Developer Opens PR -> GitHub Action Triggers -> Fetch Code Diff (PyGithub) -> Filter for `.cs` Files -> Send to Gemini API -> Receive JSON Array -> Post Inline Comments to GitHub PR.

---

## Setup Instructions

1. Copy the `.github/workflows/review.yml` (from this repository) into your own repository's `.github/workflows/` directory.
2. In your GitHub repository, navigate to **Settings** -> **Secrets and variables** -> **Actions**.
3. Click **New repository secret**.
4. Name the secret `GEMINI_API_KEY` and paste your Google Gemini API key as the value.
5. Note: `GITHUB_TOKEN` is automatically provided by the GitHub Actions runner, so you do not need to set it up manually.

---

## Run Instructions

1. Create a new branch in your repository.
2. Write or modify a C# (`.cs`) file and intentionally introduce logic flaws (e.g., Unhandled Nulls or `Task.Wait()`).
3. Commit and push your branch to GitHub.
4. Open a **Pull Request** against your main branch.
5. The GitHub Action will automatically trigger. Wait 30-40 seconds for the Action to complete.
6. Navigate to the **Files changed** tab on your Pull Request to view the automated inline comments posted by the AI reviewer.

---

## Assumptions & Limitations

### Assumptions
* **Language Support:** The system assumes the codebase uses C# and explicitly filters for `.cs` files. Other languages modified in the same PR will be ignored.
* **Environment:** The Action runs on an `ubuntu-latest` GitHub runner with a Python 3.x environment.
* **API Availability:** Assumes the Google Gemini API is highly available and responds within typical timeout bounds.

### Limitations
* **Token Limits:** If a Pull Request is exceptionally large (e.g., thousands of lines of code changed in a single file), it may exceed the Gemini API's token context window limit.
* **Cross-File Context:** The AI currently analyzes individual modified files in isolation based on the `git diff`. It may lack broader cross-file project context (e.g., recognizing that a method is defined differently in another un-modified file).
* **False Positives:** Like all LLMs, the AI may occasionally misinterpret complex, custom architectural patterns as SOLID violations.

---

## License
This project is developed for educational and prototype demonstration purposes.
