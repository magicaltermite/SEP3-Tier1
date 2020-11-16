﻿using System.Text.Json.Serialization;

namespace SEP3_Tier1.Network
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumRequest
    {
        // Creating new objects in database
        CreateBookSale,
        CreateUser,
        
        // Getting from database
        GetBookSale,
        GetUser,
        GetCard,
        
        // Selling Book
        SellBook,
        
        // Deleting objects from database
        DeleteSale,
        DeleteUser,
        DeleteCard,
        
        // Rate user
        RateUser,

        // Prototype
        recieveProofOfConcept,
        sendProofOfConcept
        
    }
}