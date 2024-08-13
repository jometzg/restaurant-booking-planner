#pragma warning disable SKEXP0050

using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Newtonsoft.Json;

/// <summary>
/// A plugin that searches the web to get information that contains the next match date and time for the a passed football team.
/// https://learn.microsoft.com/en-us/semantic-kernel/concepts/plugins/using-data-retrieval-functions-for-rag
/// </summary>
public class BookingsPlugin //JM+
{
    private readonly List<BookingModel> _bookings = new List<BookingModel>();

    [KernelFunction("web_restaurant_search")]
    [Description("Gets information that contains suitable restaurnats near my location that have the right cuisine type.")]
    [return: Description("Information that contains a list of potential restaurants that meet the criteria.")]
    public async Task<string> WebSearch([Description("The location")] string location,
                                        [Description("The cuisine")] string cuisine)
    {
        // CHALLENGE 2.2
        // get information that contains suitable restaurnats near my location that have the right cuisine type.
        var kernel = Kernel.CreateBuilder().Build();
        
        var bingConnector = new BingConnector("8d54bec91c57418bb4e8617efce7b5de");
        kernel.ImportPluginFromObject(new WebSearchEnginePlugin(bingConnector), "bing");

        var function = kernel.Plugins["bing"]["search"];
        var bingResult = await kernel.InvokeAsync(function, new() { ["query"] = "What is the name of a restaurant that serves  "+ cuisine + "food near " + location + " that have a good rating" });
        return bingResult.ToString();
    }

    [KernelFunction("web_restaurant_add_booking")]
    [Description("Gets information that contains suitable restaurnats near my location that have the right cuisine type.")]
    [return: Description("Information that contains a list of potential restaurants that meet the criteria.")]
    public async Task<string> WebAddBooking([Description("The restaurant")] string restaurant,
                                        [Description("The date of booking")] string date_booking,
                                        [Description("The time of booking")] string time_booking,
                                        [Description("The number of people")] int number_of_people,
                                        [Description("The customer name")] string customer_name,
                                        [Description("The customer email")] string customer_email)
    {
        // CHALLENGE 2.3
        // Write a native function that creates a booking for a restaurant.
        var booking = new BookingModel
        {
            Id = _bookings.Count + 1,
            RestaurantName = restaurant,
            BookingDate = date_booking,
            BookingTime = time_booking,
            NumberOfPeople = number_of_people,
            CustomerName = customer_name,
            CustomerEmail = customer_email
        };
        _bookings.Add(booking);
        return JsonConvert.SerializeObject(booking);
    }

    [KernelFunction("web_restaurant_get_booking")]
    [Description("Gets the details of a booking for a restaurant when given the booking id.")]
    [return: Description("Information about the booking.")]
    public async Task<string> WebGetBooking([Description("The booking id")] int booking_id)
    {
        // CHALLENGE 2.3
        // Write a native function that gets a booking for a restaurant.
        var booking = _bookings.FirstOrDefault(b => b.Id == booking_id);
        return JsonConvert.SerializeObject(booking);
    }

    [KernelFunction("web_restaurant_delete_bookings")]
    [Description("Deletes a restaurant booking when passed the booking id.")]
    [return: Description("Success status.")]
    public async Task<string> WebDeleteBooking([Description("The booking id")] int booking_id)
    {
        // CHALLENGE 2.3
        // Write a native function that deletes a booking for a restaurant.
        var booking = _bookings.FirstOrDefault(b => b.Id == booking_id);
        if (booking != null)
        {
            _bookings.Remove(booking);
            return "Booking deleted";
        }
        return "Booking not found";
    }
}