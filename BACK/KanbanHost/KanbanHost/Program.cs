var builder = DistributedApplication.CreateBuilder(args);



var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var db = sql.AddDatabase("database");

var api=builder.AddProject<Projects.KanbanApi>("api")
    .WithReference(db)
    .WaitFor(db)
    .WithExternalHttpEndpoints();


builder.AddYarnApp("yarn-demo", "../../../FRONT")
    .WithReference(api)
    .WithEnvironment("NODE_OPTIONS", "--openssl-legacy-provider")
    .WithEnvironment("REACT_APP_API_URL",api.Resource.GetEndpoint("http"))
    .WithYarnPackageInstallation()
    .WithExternalHttpEndpoints();

builder.Build().Run();