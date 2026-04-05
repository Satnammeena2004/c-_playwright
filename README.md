# Playwright with C# — Final Assessment

**Candidate:** STNAM  
**Training:** Playwright with C# — 1-Week Plan  
**Target site:** https://practicesoftwaretesting.com  
**Framework:** NUnit + Playwright for .NET 8

---

## Project Structure

```
PlaywrightAssessment/
├── Config/
│   └── Config.cs               ← BaseUrl, credentials from env vars
├── Pages/
│   ├── BasePage.cs             ← Screenshot + WaitForLoad helpers
│   ├── LoginPage.cs            ← LoginAs(), all login locators
│   └── ProductPage.cs          ← Search, AddToCart, product locators
├── Tests/
│   ├── BaseTest.cs             ← Trace start/stop, screenshot on failure
│   ├── GlobalUsings.cs         ← Global using statements
│   ├── SmokeTests.cs           ← Day 1: homepage, title, nav, screenshot
│   ├── LoginTests.cs           ← Task 1: positive, negative, data-driven
│   ├── CheckoutFlowTests.cs    ← Task 2: E2E search → cart flow
│   ├── ApiTests.cs             ← Task 3: GET/POST API validation
│   └── NetworkMockTests.cs     ← Day 5: route interception + mocking
├── Utilities/
│   └── AuthHelper.cs           ← Save/load storage state for fast login
├── Artifacts/                  ← Screenshots, traces, reports (auto-created)
├── .github/workflows/
│   └── playwright.yml          ← GitHub Actions CI pipeline
├── PlaywrightAssessment.csproj
└── README.md
```

---

## Prerequisites

- .NET 8 SDK
- PowerShell (pwsh) for browser installation
- Git

---

## Setup & Run

### 1. Restore packages
```bash
dotnet restore
```

### 2. Build
```bash
dotnet build
```

### 3. Install Playwright browsers
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### 4. Run all tests
```bash
dotnet test
```

### 5. Run with HTML report
```bash
dotnet test --logger "html;logfilename=report.html" --results-directory TestResults
```

### 6. Run only smoke tests
```bash
dotnet test --filter "TestCategory=Smoke"
```

### 7. Run headed (see the browser)
```bash
HEADED=1 dotnet test
```

---

## Environment Variables (optional override)

| Variable | Default |
|---|---|
| `BASE_URL` | https://practicesoftwaretesting.com |
| `CUSTOMER_EMAIL` | customer@practicesoftwaretesting.com |
| `CUSTOMER_PASSWORD` | welcome01 |
| `API_BASE_URL` | https://dummyjson.com |

---

## Key Design Decisions

1. **POM pattern** — All pages have their own class. Tests never write locators directly.
2. **BaseTest** — Every test class inherits this. Traces and screenshots are captured automatically on failure.
3. **Storage state** — `AuthHelper` saves a login session once; `CheckoutFlowTests` reloads it to skip UI login.
4. **Data-driven tests** — `TestCaseSource` feeds multiple input sets into one test method.
5. **Role-based locators** — `GetByRole`, `GetByLabel`, `GetByPlaceholder` preferred over CSS/XPath.
6. **No hardcoded waits** — `Task.Delay` / `Thread.Sleep` are never used.

---

## Assessment Coverage

|  |  | Status |
|---|---|---|
|    Login positive + negative |  | LoginTests.cs |
|  E2E business flow |  | CheckoutFlowTests.cs |
|  API validation |  | ApiTests.cs |
|  Trace + screenshot on failure |  | BaseTest.cs |
|  Structure + README |  | This file |

---

## Viewing Failure Traces

```bash
pwsh bin/Debug/net8.0/playwright.ps1 show-trace Artifacts/trace-<TestName>.zip
```
