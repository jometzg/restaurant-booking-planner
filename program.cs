#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0060

using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


// Create the kernel
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "<YOUR_MODEL_DEPLOYMENT",
    "https://<YOUR_AZURE_OPENAI_SERVICE>.openai.azure.com/",
    "<OPENAI_KEY>");

builder.Plugins.AddFromType<BookingsPlugin>();
Kernel kernel = builder.Build();
var prompts = kernel.ImportPluginFromPromptDirectory("prompts");

// Retrieve the chat completion service from the kernel
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// 2. Enable automatic function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() 
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Create the chat history
ChatHistory history = new ChatHistory("""
    You are a friendly assistant who likes to help a user locate a restaurant of a specific cuisine
    in a location of their choosing, to then book a restaurant using a booking service atg the restaurant webite URL. 
    Then to simulate the confirmation email that the restaurant will send to the user.
    If the user doesn't provide enough information for you to complete a task, you will keep asking questions until 
    you have enough information to complete the task.
    Start the conversation by asking the user what they would like to do.
    """);


string? userInput;
do {
    // Collect user input
    Console.Write("User > ");
    userInput = Console.ReadLine();

    // Add user input
    history.AddUserMessage(userInput);

    // 3. Get the response from the AI with automatic function calling
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Assistant > " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);
