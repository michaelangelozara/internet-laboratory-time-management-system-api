A **production-ready monolithic ASP.NET Core solution template** built using **Clean Architecture** principles.

This template is designed for developers who value **maintainability, scalability, testability, and long-term correctness** over quick hacks.  
It provides a solid starting point for building enterprise-grade APIs while keeping architectural boundaries explicit and enforceable.

---

## ✨ Key Features

- 🧱 **Monolithic architecture** with clear vertical and horizontal boundaries
- 🧼 **Clean Architecture** (Domain, Application, Infrastructure, Presentation)
- 🔁 Dependency inversion enforced via abstractions
- 🧪 Test-friendly structure
- ⚙️ Ready to be installed as a **dotnet template**
- 🚀 Optimized for long-term evolution, not throwaway projects

---

## 🏗️ Architecture Overview

The solution follows Clean Architecture principles:

src/
├── Domain → Enterprise business rules
├── Application → Use cases, interfaces, DTOs
├── Infrastructure → External concerns (DB, APIs, Email, etc.)
└── WebApi → ASP.NET Core entry point

**Dependency flow** is strictly inward:

No layer depends on concrete implementations from outer layers.

---

## 📦 Template Metadata

This template is registered with the following configuration:

- **Template Name:** AspNet Clean Architecture Solution
- **Short Name:** `aspnetcleanarch`
- **Language:** C#
- **Type:** Solution
- **Author:** Michael Angelo Buccat Zara

---

## 🚀 Getting Started

### 1️⃣ Clone the Repository

Using Command Prompt, PowerShell, or any terminal:

```
git clone https://github.com/<your-username>/monolith-clean-architecture-aspnet-core-template.git

cd monolith-clean-architecture-aspnet-core-template
```

### 2️⃣ Install the Template Using .NET CLI

Using Command Prompt, PowerShell, or any terminal:

```
dotnet new install .
```

You can also verify using this .NET command:

```
dotnet new --list
```

Look for:
```
AspNet Clean Architecture Solution   aspnetcleanarch
```

### 3️⃣ Create a New Solution Using the Template

Navigate to the directory where you want your new project:

```
cd path/to/your/projects
```

Then run:
```
dotnet new aspnetcleanarch -n MyAwesomeProject
```

This will:

Create a new solution named MyAwesomeProject

Replace all NDTC.InternetLaboratoryTimeManagementSystem placeholders

Generate a ready-to-run Clean Architecture solution

### 4️⃣ Open and Run the Solution

Navigate into the project:

```
cd MyAwesomeProject
```

Restore dependencies:

```
dotnet restore
```

Run the API:

```
dotnet run --project src/WebApi
```
