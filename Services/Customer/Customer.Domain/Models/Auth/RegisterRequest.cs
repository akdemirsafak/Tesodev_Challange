namespace Customer.Domain.Models.Auth;

public record RegisterRequest(string Name, string Email, string Password);
