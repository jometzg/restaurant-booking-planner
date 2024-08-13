using System.Text.Json.Serialization;

public class BookingModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("restaurant_name")]
    public string? RestaurantName { get; set; }

    [JsonPropertyName("booking_date")] 
    public string? BookingDate { get; set; }

    [JsonPropertyName("number_of_people")]
    public int NumberOfPeople { get; set; }
   
    [JsonPropertyName("booking_time")]
    public string? BookingTime { get; set; }

    [JsonPropertyName("customer_name")]
   public string? CustomerName { get; set; }

   [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }
}

