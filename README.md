<p align="center">
  <a href="https://github.com/discord-net/Discord.Net">
    <img width="100" src="https://github.com/zobweyt/Giveaways/assets/98274273/94c82d8b-3593-4401-acb9-b791cf849761" alt="App icon" />
  </a>
</p>

<h1 align="center">
  Giveaways
</h1>

<p align="center">
  An app for hosting fair and reliable giveaways at your server.
</p>

<p align="center">
  <a href="https://discord.com/invite/aAn3KkaJzM">
    <img src="https://dcbadge.vercel.app/api/server/aAn3KkaJzM?style=flat&theme=clean-inverted" alt="Discord" />
  </a>
  <img src="https://img.shields.io/librariesio/github/zobweyt/Giveaways" alt="Dependencies" />
  <img src="https://img.shields.io/github/created-at/zobweyt/Giveaways" alt="Creation Date" />
</p>

## 🗝️ Features
Coming soon…

## 🗺️ Roadmap

To see the current and future tasks for this project, please navigate to the [projects](https://github.com/zobweyt/Giveaways/projects) tab.

## 📦 Personal usage

To start, open a command prompt and follow these instructions:

### Step 1 — Get the app

Clone this repository to your machine, open it in your editor, and navigate to the startup project:

```sh
git clone https://github.com/zobweyt/Giveaways.git
cd ./src/Giveaways
```

### Step 2 — Configure the environment

We are using the [options pattern](https://learn.microsoft.com/aspnet/core/fundamentals/configuration/options) for typed access to groups of [related settings](./src/Giveaways/Common/Options). You should configure the [`appsettings.json`](./src/Giveaways/appsettings.json) file or manage [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) via [CLI](https://learn.microsoft.com/dotnet/core/tools):

```sh
dotnet user-secrets set <key> <value>
```

> [!NOTE]
> Pending database migrations are applied automatically [before startup](./src/Giveaways/Program.cs#L49) and an informational [message](./src/Giveaways/Extensions/HostExtensions.cs#L29) is logged.

### Step 3 — Run the app

To run the bot, just execute the following command: 

```sh
dotnet watch
```

The setup is done. Enjoy using the app! 🎉

> [!WARNING]
> Instead of using the `dotnet run` in production, create a deployment using the `dotnet publish` command and [deploy](https://docs.discordnet.dev/guides/deployment/deployment) the output.

## 🚀 Contributing

To contribute to this project, please read the [`CONTRIBUTING.md`](.github/CONTRIBUTING.md) file. It provides details on our code of conduct and the process for submitting pull requests.

## ❤️ Acknowledgments

See the [contributors](https://github.com/zobweyt/Giveaways/contributors) who participated in this project and the [dependencies](https://github.com/zobweyt/Giveaways/network/dependencies) used.

## 📜 License

This project is licensed under the **MIT License** — see the [`LICENSE.md`](LICENSE.md) file for details.
