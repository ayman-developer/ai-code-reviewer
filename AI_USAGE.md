## AI Usage Note
Project Title
## AI Code Review GitHub Action for C#

## Purpose
This document describes how Artificial Intelligence was used during both the development phase and the final execution of the AI Code Review GitHub Action.

Our project leverages an AI-assisted development workflow and integrates Google's Gemini LLM directly into the CI/CD pipeline to automate C# code analysis, detect security vulnerabilities, and provide inline pull request feedback.

## AI Tools Used During Development
The following AI tools were utilized to accelerate our development process:

Gemini: Used for generating the core Python backend logic (review_agent.py) and formulating the C# code review prompts.

ChatGPT: Used for designing the GitHub Actions YAML workflow (ai-review.yml) and troubleshooting GitHub repository permissions and branch protection rules.

Claude: Used for formatting professional project documentation, README files, and structuring submission reports.

Kiro: Used during the initial requirement analysis and workflow conceptualization phase.

## AI-Assisted Development Activities
Artificial Intelligence acted as a pair-programming assistant in the following areas:

1. CI/CD Architecture & Setup
AI tools assisted in designing the GitHub Action runner. It helped us write the exact .yml configurations needed to trigger the action only on pull_request events and handle the checkout of the repository securely.

2. Backend Script Development
We used AI to help develop the Python script that bridges GitHub and the Gemini API. AI assisted in writing the logic to extract the exact code differences (diffs) from a Pull Request and send them efficiently to the LLM.

3. Prompt Engineering
A significant portion of development involved crafting the perfect system prompt for the Gemini API. AI helped us refine our prompts to ensure the reviewer strictly checks for C# SOLID principles, SQL injections, unmanaged resource leaks, and NullReferenceExceptions.

4. Test Case Generation
We utilized AI to generate complex, enterprise-level C# sample files containing hidden, intentional bugs (e.g., hardcoded passwords and swallowed exceptions) to rigorously test our AI Reviewer's detection capabilities.

## AI Capability Implemented
Unlike traditional static code analyzers (like SonarQube), our project implements Generative AI for dynamic code understanding.

The project demonstrates the following core AI capabilities:

Contextual Vulnerability Detection
The AI engine understands the context of the C# code. It can identify complex security risks such as raw string concatenation in SQL queries and hardcoded secrets within connection strings.

Automated Code Remediation
Instead of just throwing an error code, the AI generates accurate, corrected C# code snippets. It provides the exact parameterized query or null-check logic needed, allowing the developer to easily fix the issue.

Natural Language Summarization
The AI translates complex code flaws into easy-to-read, human-like summaries formatted cleanly in Markdown, which are then posted directly as a GitHub comment.

## Agent Workflow Implementation
The project acts as a single, fully autonomous CI/CD Review Agent. The workflow is completely automated without manual triggers:

Trigger: A developer commits buggy C# code and opens a Pull Request to the main branch.

Action Runner: GitHub Actions detects the PR and provisions an Ubuntu runner.

Data Extraction: The Python script extracts the modified C# code.

AI Inference: The code is sent to the Gemini 1.5 API securely using Repository Secrets.

Output Generation: The AI processes the code, identifies vulnerabilities, and formulates a structured review.

Integration: The GitHub github-actions[bot] automatically posts the AI's review as a comment on the PR.

Branch Protection: Based on the review, human developers can safely approve or block the merge.

## AI Runtime Engine
Model: Google Gemini 1.5 Flash (via API)

Environment: GitHub Actions CI/CD Cloud Runner

Why this architecture: By using a cloud API, the GitHub Action remains lightweight, executes in under 30 seconds, and does not require heavy local GPU resources.

Conclusion
Artificial Intelligence was heavily utilized to build the infrastructure (YAML, Python scripts, API integration) of this project. Ultimately, the deployed product itself serves as an AI Agent, demonstrating how LLMs can be seamlessly integrated into modern DevOps pipelines to drastically reduce manual review times, catch critical security flaws early, and enforce clean coding standards before code ever reaches production.
