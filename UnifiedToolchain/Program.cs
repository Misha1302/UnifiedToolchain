using Bootstrapper;

CurrentDirectory.Init();

const string uri = "http://localhost:5019/";
await PluginsRunner.Instance.Run("Bootstrapper/bin/net8.0/Bootstrapper",
    uri
);

Services.RegisterServer<IBootstrapper>(uri);

MeasuringTimer.Instance.Measure(() =>
    Request<IBootstrapper>.Instance.ImportConfiguration(
        new Json(new { Path = "UnifiedToolchain/toolchain_configuration.json" })
    )
).Print("ImportConfiguration took {0}ms");

MeasuringTimer.Instance.Measure(() =>
    Request<IBootstrapper>.Instance.GetPlugins(Json.Empty)
).Print("GetPlugins took {0}ms");