# WorkflowCore.Http

.NET CORE 3.1 library that defines HTTP workflow steps for the [WorkflowCore](https://github.com/danielgerlag/workflow-core/) workflow engine

# Usage

[Nuget Package](https://www.nuget.org/packages/WorkflowCore.Http/)

```
  dotnet add package WorkflowCore.Http
```

# Sample code

## Configuration

```C#
public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddWorkflow();
        services.AddHttpWorkflow();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IWorkflowHost workflowHost)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        app.UseRouting();
        app.UseHttpWorkflow();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        workflowHost.Start();
    }

}
```

## Workflow usage

```C#
builder.StartWith(context =>
{
    Console.WriteLine("Begining saga");
})
    .Saga(saga =>
    {
        saga
            .StartWith(context => Console.WriteLine("Saga started"))
            .Then<SendHttpRequest>(setup =>
            {
                setup.Input(step => step.Method, data => HttpMethod.Post);
                setup.Input(step => step.Uri, data => "http://localhost/workflow/execute");
                setup.Input(step => step.RetryPolicy, data => Policy
                    .Handle<HttpRequestException>(ex => ex.Message.Contains("400"))
                    .RetryAsync(4, (ex, attempts) => Console.WriteLine($"Attempt n°{attempts}")));
                setup.Input(step => step.Content, data => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, MediaTypeNames.Application.Json));
            })
                .CompensateWith<SendHttpRequest>(setup =>
                    {
                        setup.Input(step => step.Method, data => HttpMethod.Get);
                        setup.Input(step => step.Uri, data => "http://localhost:56832/workflow/compensate");
                    })
            .Then<ReceiveHttpRequest>(setup =>
            {
                setup.Input(step => step.Method, data => HttpMethod.Get);
                setup.Input(step => step.Path, data => "/test/go");
            })
            .Then(context => Console.WriteLine("Saga ended"));
    });
```


# Contributing

Please see [CONTRIBUTING.md](https://github.com/neuroglia-io/WorkflowCore.Http/blob/master/CONTRIBUTING.md) for instructions on how to contribute.
