# CreatePdfs.Playwright

This project demonstrates how to use Playwright in a .NET 9 application to generate PDFs from HTML templates.

It showcases the integration of Playwright for browser automation and rendering tasks,
specifically for creating PDF documents.

## Purpose of the Demonstration

The purpose of this demonstration is to provide a practical example of how to:

- Use Playwright in a .NET application.
- Render HTML templates into PDF documents.
- Automate browser tasks using Playwright.

## How to Run the Demonstration

Follow these steps to run the demonstration on your local machine:

### Prerequisites

1. Ensure you have .NET 9 installed on your system. You can download it from the [official .NET website](https://dotnet.microsoft.com/).
2. After restoring the dependencies, Install Playwright dependencies by running the following command in the terminal:

   ```powershell
   .\bin\Debug\net9.0\playwright.ps1 install chromium --with-deps --only-shell
   ```

1.    This command installs the Chromium browser along with its dependencies required by Playwright.

### Steps to Run

1. Clone this repository to your local machine.
2. Open the project in your preferred IDE (e.g., Visual Studio or Visual Studio Code).
3. Build the project to restore dependencies and compile the code:

   ```powershell
   dotnet build
   ```

4. Run the application:

   ```powershell
   dotnet run
   ```

The application will generate a PDF using the provided `html-template.html` file,
save it to the output directory and open the file using the application registered on your system.

## Note

- If you encounter any issues, verify that all dependencies are installed and that the Playwright installation step was completed successfully.