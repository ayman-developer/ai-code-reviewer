# AI Usage Note

## What AI helped with
Artificial Intelligence (specifically Gemini) was heavily utilized throughout the development of this prototype, both as a coding assistant and as the core functional engine of the final product.

**During Development:**
* **Workflow Architecture:** The AI assistant helped design the sequence of events (fetching the `git diff`, formatting the payload, pushing comments via the GitHub API).
* **Python Scripting:** The AI assistant wrote the `review_agent.py` script, utilizing the `PyGithub` library to interface with Pull Requests.
* **DevOps:** The AI generated the `Dockerfile` and `action.yml` to ensure the Python script successfully runs as an isolated GitHub Action.

**During Execution (The Core Engine):**
* **Code Analysis:** The Gemini 2.5 Flash API is the brain of the prototype, responsible for analyzing raw `.cs` diffs.
* **Logic Auditing:** Gemini successfully detects hidden NullReference traps and thread-blocking Deadlocks that standard linters miss.
* **JSON Formatting:** The AI was successfully instructed to return its output in a strict `application/json` array, enabling the Python script to parse the output and post inline GitHub comments.

---

## What AI got wrong
While building and testing the prototype, the AI encountered a few hurdles and produced some errors that required human correction and prompt tuning:

1. **GitHub Action Permissions:** Initially, the AI-generated `action.yml` failed to run because it lacked explicit write permissions for Pull Requests. We had to manually add `permissions: pull-requests: write` to the workflow file.
2. **Global vs. Inline Comments:** The AI initially attempted to post the code review as one massive, unformatted global comment at the bottom of the PR. This was difficult to read. We had to heavily correct the AI and prompt it to parse specific line numbers to use GitHub's inline commenting API instead.
3. **Over-zealous SOLID Critiques:** During early testing, the AI would sometimes flag perfectly fine, standard C# code (like basic DTOs) as "violating the Single Responsibility Principle". We had to tighten the prompt to ensure it only focused on severe architectural violations to prevent "noise" and false positives.
4. **Markdown inside JSON:** The AI sometimes attempted to return markdown code blocks (e.g., \`\`\`json ) surrounding the raw JSON response, which broke the Python `json.loads()` parser. We had to update the code to explicitly strip markdown formatting before parsing.

---

## Best prompts used

### 1. The Core Agent Prompt (System Instruction)
This is the most critical prompt in the project. It is hardcoded into the Python script and acts as the "Brain" of the GitHub Action. It forces the LLM to behave like a strict auditor and output parsable JSON.
> "You are an expert senior enterprise C# software engineer. Review this code patch for standard programming logic failures: look for unhandled null exceptions, breaking async patterns, and violations of clean code design. Return your response in a clean, parsable JSON array containing the target file name, line number, and a detailed code fix description.
> 
> JSON Format Example:
> [
>   {"file": "Program.cs", "line": 25, "description": "This async method uses .Wait() which causes deadlocks. Use await instead."}
> ]"

### 2. The Refactoring Prompt (Development Phase)
This prompt was used during development to instruct the AI coding assistant to upgrade the prototype to post inline comments instead of global comments.
> "I need to align the prototype exactly with the official Challenge Blueprint. Refactor `review_agent.py` so that it explicitly filters for only `.cs` files to save tokens. Then change the Gemini prompt to output a JSON array with file, line, and description. Finally, write the logic to parse that JSON array and post inline review comments directly onto the developer's specific lines of code in the GitHub pull request web interface."

### 3. The Test Case Generator Prompt
This prompt was used to generate flawed data to prove the AI reviewer worked correctly.
> "Give me a very advanced, enterprise-level C# code snippet that intentionally contains subtle flaws related to SOLID principles, Null-Handling, and Async/Await deadlocks so I can use it to test my AI Code Reviewer. Disguise the flaws so they look like normal code."
