# AI Code Reviewer Prototype

A working prototype for the AI Prototype Challenge. This repository contains an AI-powered GitHub Action that automatically reviews Pull Requests specifically focusing on C# best practices.

## Deliverables Met

1. **Fully Functional Prototype**: This repository contains a custom GitHub Action that successfully intercepts `pull_request` events, pulls the git diff, and posts an automated code review comment.
2. **AI Development Notes**: All AI interactions used to build this prototype are documented in [DEVELOPMENT_NOTES.md](./DEVELOPMENT_NOTES.md), fulfilling the requirement to document hands-on AI assistant usage.
3. **Mandatory Capability Implemented**: **External API/Service Integration**. The Action integrates with the official Google GenAI API (Gemini) to process code diffs.
4. **Demonstration**: You can view the automated reviews in the closed Pull Requests of this repository.

## Technology Stack Used
- **Source Control**: GitHub
- **Language**: Python
- **AI Model**: Google Gemini (Free Tier API)
- **Integrations**: GitHub REST API (via `PyGithub`), Google GenAI API (`google-genai`).

## How it Works
When a Pull Request is opened or synchronized, the Action:
1. Runs a Dockerized Python script (`review_agent.py`).
2. Connects to the GitHub repository using the native `GITHUB_TOKEN`.
3. Fetches the code diff of the Pull Request.
4. Sends the diff to the Gemini API with a strict system prompt to analyze for **SOLID Principles**, **Null-Handling**, and **Async/Await** implementations.
5. Posts the analysis back to the Pull Request as a formatted comment.

## Setup Instructions
If you fork this repository to use the Action, you must:
1. Obtain a free API key from [Google AI Studio](https://aistudio.google.com/app/apikey).
2. Go to your repository **Settings** -> **Secrets and variables** -> **Actions**.
3. Add a New Repository Secret named `GEMINI_API_KEY` with your API key.
4. Ensure your workflow permissions allow writing pull requests (see `.github/workflows/ai-review.yml`).