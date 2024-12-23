namespace authentication_management.database.models;

public record User(
    string StaffUniqueId,
    string Username,
    string Designation,
    string[] Roles);