using TidyHPC.Loggers;
using TidyHPC.Routers;
using TidyHPC.Routers.Args;
using VizGroup;
using VizGroup.V1;
Logger.FilePath = Path.Combine(Util.CreateDirectory(Path.Combine(Util.GetDataDirectory(),"Logs")),$"{Path.GetFileName(Environment.ProcessPath)}-{DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss")}.log");
Logger.QueueLogger.OnLine += async (message) =>
{
    Console.WriteLine(message);
    await Task.CompletedTask;
};

ArgsRouter argsRouter = new();

async Task startServer(
    [ArgsAliases("--port")] string port,
    [ArgsAliases("--cert")] string certPath = "",
    [ArgsAliases("--cert-password")] string certPassword = "")
{
    Application application = new();
    try
    {
        await application.Start(new()
        {
            ServerPorts = [int.Parse(port)],
            SSLCertificatePath = certPath,
            SSLCertificatePassword = certPassword,
            EnablePlugins = false
        });
    }
    catch (Exception e)
    {
        Logger.Error(e);
        throw;
    }
}

argsRouter.Register(["server"], startServer);

async Task client(
    [ArgsAliases("--port")] string port,
    [ArgsAliases("--share-server-url-prefix")] string shareServerUrlPrefix,
    [ArgsAliases("--database-path")] string? databasePath = null,
    [ArgsAliases("--plugins-directory")] string? pluginsDirectory = null)
{
    Application application = new();
    try
    {
        await application.Start(new()
        {
            ServerPorts = [int.Parse(port)],
            EnablePlugins = true,
            EnableShareServer = true,
            ShareServerUrlPrefix = shareServerUrlPrefix,
            DatabasePath = databasePath,
            PluginsDirectory = pluginsDirectory
        });
    }
    catch (Exception e)
    {
        Logger.Error(e);
        throw;
    }
}

argsRouter.Register(["client"], client);

await argsRouter.Route(args);
