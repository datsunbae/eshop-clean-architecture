namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record class UserInfomation(
    string UserName,
    Phone UserPhone,
    Address OrderAddress);
