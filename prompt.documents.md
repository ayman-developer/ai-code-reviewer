## Prompt Documentation
Project Title
## AI Code Review GitHub Action for C#

## Purpose
This document records the major prompts used during the development and execution of the AI Code Review GitHub Action.

Our project uses AI-assisted development for infrastructure setup and AI-powered dynamic code analysis through a cloud Large Language Model (Google Gemini 1.5 API).

## The documented prompts were used for:

CI/CD pipeline generation

Automated code review and vulnerability detection

Code remediation and fix suggestions

Project documentation and formatting

Prompt engineering

Single-agent automated workflow design

AI Development Usage
AI coding assistants (ChatGPT, Claude, Gemini, Kiro) were used throughout development for:

GitHub Actions YAML configuration

Python backend development (review_agent.py)

Prompt engineering for the Gemini LLM

Debugging GitHub repository permissions

Test case creation (C# vulnerabilities)

Document structuring

The development process followed an AI-assisted workflow as required by the challenge guidelines.

##Prompt 1: Challenge Constraint & Architecture Setup
Objective
Define the overarching architecture and challenge constraints for the AI Prototype to ensure strict compliance with mandatory hackathon requirements, including AI usage documentation, Agent Loop implementation, and API integrations.

Prompt

Plaintext
You are an AI development expert tasked with creating a solution for the AI Prototype Challenge. Your goal is to build a working prototype that meets all mandatory requirements and evaluation criteria.

**Core Objective:**
Design and document a complete AI solution prototype that demonstrates hands-on capability in AI agents, MCPs (Model Context Protocol), or external API/service integration. The solution must be shipping-ready code built using AI coding assistants.

**Mandatory Requirements (Non-Negotiable):**
1. Use AI coding assistants (e.g., Claude, ChatGPT, Cursor, GitHub Copilot) throughout development—document this in your development notes
2. Maintain a notes file recording all key prompts used during development with AI assistants
3. Implement at least ONE of these capabilities:
   - Agent Loop (autonomous agent with decision-making/iteration)
   - MCP Tool (either built or consumed)
   - External API/Service Integration

**Technology Stack (Free/Open-Source Only):**
- Source Control: GitHub
- Languages: Python, Node.js, or .NET
- Database: SQLite or PostgreSQL (Docker)
- AI Models: Ollama or Free LLM Tiers
- Data Sources: CSV/JSON
- Integrations: Public APIs
- Collaboration: Discord

**Evaluation Criteria (What Success Looks Like):**
- Working code shipped with demonstrated AI assistant usage
- Hands-on skill in building AI Agents or MCPs
- Successful Service/API Integration capability
- End-to-end execution with real usability (not just scaffolding)
- Code quality, clear documentation, and ability to demonstrate the solution

**Deliverables:**
1. Fully functional prototype code in a GitHub repository
2. Development notes file showing all key AI assistant prompts used
3. Clear documentation of how your solution meets the mandatory requirements
4. A demonstration of the working prototype in action

**Context from Challenge Files:**
Reference the challenge specifications in "WhatsApp Image 2026-06-08 at 5.37.59 PM.jpeg" and "WhatsApp Image 2026-06-08 at 5.37.58 PM.jpeg" for complete mandatory requirements and technology stack details. You may also reference "image.png" for an example solution pattern (AI Code Review GitHub Action).

**Timeline:** This is a 1-2 day sprint. Focus on working functionality over perfection.

Build something real, document your AI usage thoroughly, and ship it.
Prompt 2: GitHub Actions CI/CD Pipeline Generation
Objective
Develop a complete, production-ready GitHub Actions YAML workflow that automates code diff extraction, secure Gemini API integration, and posts structured AI review comments directly to Pull Requests.

Prompt

Plaintext
You are a GitHub Actions workflow engineer and AI integration specialist. Your task is to create a complete, production-ready GitHub Actions workflow file that I can add to my repository to enable automated AI code review using Google Gemini.

**Context:**
I have an AI Code Reviewer project that uses the Gemini API to automatically review C# code for violations in three categories:
1. SOLID principles (Single Responsibility, Dependency Inversion, etc.)
2. Null-handling errors and potential NullReferenceExceptions
3. Async/await threading issues, deadlocks, and improper async patterns

**Your Task:**
Create a complete `.github/workflows/code-review.yml` file that:

1. **Triggers** on every Pull Request that modifies `.cs` files
2. **Extracts** all changed C# code from the PR
3. **Connects to the Gemini API** securely using GitHub Secrets (the API key should be stored as `GEMINI_API_KEY`)
4. **Sends the code** to Gemini with a system prompt instructing it to act as an expert C# reviewer looking specifically for SOLID violations, null-handling bugs, and async/await issues
5. **Posts the AI's review** as an automated comment on the Pull Request in a clean, formatted table that clearly lists:
   - Issue category (SOLID / Null-Handling / Async-Await)
   - Specific line or code snippet
   - Severity (Critical / Warning / Info)
   - Recommendation for fixing it
6. **Handles errors gracefully** - if the API call fails, it should still post a fallback message to the PR
7. **Runs efficiently** - should complete the analysis within 60 seconds for typical code submissions

**Technical Requirements:**
- Use `actions/checkout@v4` to access the repository code
- Use the official Google Generative AI API (not a third-party wrapper)
- Extract only the changed lines from the PR (use `git diff`)
- Format the Gemini prompt to be strict and expert-level in tone
- Post results back to the PR using the GitHub API
- Store the API key securely as a GitHub Secret
- Include proper logging for debugging

**Output Format:**
Return the complete, ready-to-use YAML workflow file. Include comments explaining each section. Make it production-ready so I can copy-paste it directly into `.github/workflows/code-review.yml` and it will work immediately after I set the `GEMINI_API_KEY` secret in my repository settings.
Agent Loop Design
The project implements an automated, single-agent CI/CD workflow.

Agent: AI Code Reviewer (Gemini API)

Responsibilities:

Extract code differences (diffs) from GitHub Pull Requests.

Analyze C# code for SOLID principles and security flaws (SQL Injection, Hardcoded Secrets, etc.).

Identify potential runtime errors (e.g., NullReferenceExceptions).

Generate corrected C# code snippets.

Formulate a structured Markdown report.

Output:

Final Code Review Report posted automatically as a GitHub PR comment.

Agent Workflow

Developer Commits C# Code

Pull Request Opened (main branch)

GitHub Action Workflow Triggered

Code Diffs Extracted via Python Script

AI Reviewer Agent (Gemini API) Processes Code

Final Markdown Report Generated

GitHub Bot Posts Review on PR

This architecture demonstrates a fully autonomous CI/CD AI Agent capability.

Prompt Engineering Techniques Used
Role Prompting: Assigning specific enterprise responsibilities to the AI (e.g., "You are an AI development expert", "You are a GitHub Actions workflow engineer").

Structured Output Prompting: Instructing the model to produce output using a specific visual structure, such as a formatted markdown table.

Constraint-Based Prompting: Providing explicit rules to enforce challenge mandates, handle API failures gracefully, run within strict time limits (under 60 seconds), and secure API keys.

Task Decomposition: Breaking down the hackathon requirements into clear deliverables (Code, Notes, Documentation, Demo) and stepping out the exact logic flow for the CI/CD pipeline.

AI Capabilities Demonstrated
The project demonstrates:

✅ Dynamic Code Analysis

✅ Automated Vulnerability Detection

✅ Code Remediation & Fix Generation

✅ CI/CD AI Agent Architecture

✅ AI-Assisted Infrastructure Setup

✅ Prompt Engineering

Development Tools
Programming Languages: Python, C#

CI/CD Platform: GitHub Actions

AI API Runtime: Google Gemini 1.5 Flash

Data Formats: YAML, Markdown, JSON (API Payloads)

Conclusion
The AI Code Review GitHub Action uses advanced prompt engineering and an automated CI/CD agent architecture to streamline software development.

The combination of dynamic vulnerability detection, automated code remediation, and seamless integration into the GitHub Pull Request workflow provides an enterprise-grade DevSecOps solution that strictly adheres to the AI Prototype Challenge requirements.
