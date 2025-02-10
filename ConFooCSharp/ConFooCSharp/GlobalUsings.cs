global using System.Collections.Immutable;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using ConFooCSharp.Models;
global using ConFooCSharp.Presentation;
global using ConFooCSharp.DataContracts;
global using ConFooCSharp.DataContracts.Serialization;
global using ConFooCSharp.Services.Endpoints;
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;
global using Color = Windows.UI.Color;

using Uno.Extensions.Markup.Generator;
using ConFooCSharp.Behaviors;

[assembly: Uno.Extensions.Reactive.Config.BindableGenerationTool(3)]
[assembly: GenerateMarkupForAssembly(typeof(CommandOnKeyPressBehavior))]
[assembly: GenerateMarkupForAssembly(typeof(ReversedPointerWheel))]
