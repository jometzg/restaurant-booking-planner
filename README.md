# Restaurant Search and Booking using Semantic Kernel Planner
This is a partially worked through, but working demo of restaurant search and booking a restaurant.

It uses Semantic kernel [planner](https://learn.microsoft.com/en-us/semantic-kernel/concepts/planning?pivots=programming-language-csharp) to orchestrate some prompts and a booking plugin (which simulates aspects of the booking process).

In summary, the demo can:
1. Search for a restaurant by cuisine and location using bing search
2. Pick the website URL from the first item in the Bing results
3. Simulate the booking process
4. Return a booking confirnation email from the restaurant.

All of the above orchestrated by using automated function calling as a planner. See my other [lights controlling demo](https://github.com/jometzg/semantic-kernel-planne) repo for more explanation.

## Prerequitities
1. An Azure OpenAI instance with as deployed chat model e.g. *gpt-4o*. The resource name and key are needed
2. A Bing Search service provisioned in Azure. This will then just requre the key in the booking plugin


