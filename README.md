# BlazorBlogX: Interactive Blogging Made Effortless

## Description

BlazorBlogX redefines the blogging experience with its cutting-edge integration of the Blazor framework. Seamlessly combining the power of C# and the interactivity of Blazor, this platform empowers bloggers to create dynamic, engaging content like never before.

## Installing .NET 8

### Windows:

1. Visit the [.NET downloads page](https://dotnet.microsoft.com/download) on the official Microsoft website.
2. Select the ".NET 8.x SDK" option from the available downloads.
3. Follow the installation wizard instructions.

### macOS:

1. Open a terminal window.
2. Use a package manager like [Homebrew](https://brew.sh/) to install .NET:

## Key Features

- **Intuitive Blazor Interface**: Enjoy a smooth and intuitive user interface crafted with Blazor's component-based architecture, ensuring a seamless writing and editing experience.

- **Real-time Previews**: Witness your content come to life in real-time with a live preview feature that dynamically updates as you write, allowing you to see exactly how your posts will appear.

- **Efficient Content Management**: Effortlessly manage your posts, categories, and tags through a user-friendly dashboard. With BlazorBlogX, organizing your content has never been more efficient.

- **Multi-platform Compatibility**: Write and edit on-the-go with our responsive design, ensuring your blog looks stunning on desktops, tablets, and mobile devices alike.

- **Robust Security Measures**: Rest easy knowing your content is protected by advanced security features, safeguarding your blog from unauthorized access and potential threats.

- **SEO Optimization**: Boost your blog's visibility with built-in search engine optimization tools, helping you reach a wider audience and increase your online presence.

- **Customizable Themes**: Tailor the look and feel of your blog with a range of customizable themes, allowing you to express your unique style and brand.

- **Dynamic Component Library**: Extend the functionality of your blog with a rich library of Blazor components, enabling you to add interactive elements and enhance user engagement.

## Dependencies

- Ardalis.GuardClauses (Version 4.1.1)
- Ardalis.SmartEnum (Version 7.0.0)
- AutoMapper (Version 12.0.1)
- Carter (Version 7.2.0)
- ErrorOr (Version 1.3.0)
- FluentValidation (Version 11.3.0)
- Fody (Version 6.8.0)
- Humanizer (Version 2.14.1)
- MediatR (Version 12.1.1)
- Microsoft.EntityFrameworkCore (Version 8.0.0-rc.2.23480.1)
- Microsoft.EntityFrameworkCore.SqlServer (Version 8.0.0-rc.2.23480.1)
- Newtonsoft.Json (Version 13.0.3)
- Scrutor (Version 4.2.2)
- Serilog.AspNetCore (Version 7.0.0)
- Swashbuckle.AspNetCore (Version 6.5.0)
- Syncfusion.Blazor. (Version 23.1.42)
## Getting Started

### Cloning the Project
To get started, clone the project using the following command:

`git clone https://github.com/sitien173/Dotnet8BlazorWebApp.git`

### Running Migrations
Before running the application, you'll need to apply any database migrations. Navigate to the project directory and use the following command:

`dotnet ef database update`

This will apply any pending migrations and set up the database with the necessary schema.

### Starting the Application
Once the migrations have been applied, you can start the application using the following command:

`dotnet run`

### AppSettings Configuration

This `appsettings.json` file serves as a configuration file for a .NET application. It contains various settings and parameters to customize the behavior of the application.

#### Logging Configuration

The `Logging` section specifies how the application handles logging:

- **Default Log Level**: Log messages with a severity level of "Information" and higher will be recorded.

- **Microsoft.AspNetCore Log Level**: Log messages from the `Microsoft.AspNetCore` namespace with a severity level of "Warning" and higher will be recorded.

#### Application Settings

Under the `AppSettings` section, the following configuration is defined:

- **Site URL**: The base URL of the application is set to `https://localhost:7152`.

#### Syncfusion License Key

The `SyncfusionLicenseKey` field appears to contain an encrypted or encoded license key for the Syncfusion library, which is likely used within the application.

#### Allowed Hosts

Requests from any host are permitted due to the wildcard `*` in the `AllowedHosts` field. This means the application can be accessed from any domain.

#### Database Connection String

The `ConnectionStrings` section contains the connection string for the application's database:

- **Database Type**: Microsoft SQL Server is used.

- **Server**: The database is hosted locally on `localhost` with an instance named `SQLEXPRESS` and is reachable on port `1433`.

- **Database Name**: The database is named `BlazorBlogX`.

- **Authentication**: It uses SQL Server authentication with the username `sa` and password `123`.

- **Trust Server Certificate**: The application trusts the server certificate for secure connections.

