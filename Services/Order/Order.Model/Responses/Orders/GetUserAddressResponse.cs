﻿namespace Order.Model.Responses.Orders
{
    public class GetUserAddressResponse
    {
        public Guid Id { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
    }
}
