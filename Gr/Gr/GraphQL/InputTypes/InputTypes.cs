namespace Gr.GraphQL.InputTypes
{
    // ========== ADD INPUT TYPES ==========

    public record AddTrainInput(string Number, string Route, DateTime Departure, DateTime Arrival);
    public record AddPassengerInput(string FullName, string Passport, DateTime BirthDate);
    public record AddWagonInput(int TrainId, int Number, string Type, int Capacity);
    public record AddTicketInput(int PassengerId, int TrainId, int WagonId, int SeatNumber, decimal Price, DateTime PurchaseDate, bool IsSold);
    public record AddSellerInput(string FullName, string Office);

    // ========== UPDATE INPUT TYPES ==========

    public record UpdateTrainInput(int Id, string? Number, string? Route, DateTime? Departure, DateTime? Arrival);
    public record UpdatePassengerInput(int Id, string? FullName, string? Passport, DateTime? BirthDate);
    public record UpdateWagonInput(int Id, int? TrainId, int? Number, string? Type, int? Capacity);
    public record UpdateTicketInput(int Id, int? PassengerId, int? TrainId, int? WagonId, int? SeatNumber, decimal? Price, DateTime? PurchaseDate, bool? IsSold);
    public record UpdateSellerInput(int Id, string? FullName, string? Office);

    // ========== INPUT OBJECT TYPES ==========

    public class AddTrainInputType : InputObjectType<AddTrainInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddTrainInput> descriptor)
        {
            descriptor.Name("AddTrainInput");
            descriptor.Field(f => f.Number).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Route).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Departure).Type<NonNullType<DateTimeType>>();
            descriptor.Field(f => f.Arrival).Type<NonNullType<DateTimeType>>();
        }
    }

    public class UpdateTrainInputType : InputObjectType<UpdateTrainInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateTrainInput> descriptor)
        {
            descriptor.Name("UpdateTrainInput");
            descriptor.Field(f => f.Id).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Number).Type<StringType>();
            descriptor.Field(f => f.Route).Type<StringType>();
            descriptor.Field(f => f.Departure).Type<DateTimeType>();
            descriptor.Field(f => f.Arrival).Type<DateTimeType>();
        }
    }

    public class AddPassengerInputType : InputObjectType<AddPassengerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddPassengerInput> descriptor)
        {
            descriptor.Name("AddPassengerInput");
            descriptor.Field(f => f.FullName).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Passport).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.BirthDate).Type<NonNullType<DateTimeType>>();
        }
    }

    public class UpdatePassengerInputType : InputObjectType<UpdatePassengerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdatePassengerInput> descriptor)
        {
            descriptor.Name("UpdatePassengerInput");
            descriptor.Field(f => f.Id).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.FullName).Type<StringType>();
            descriptor.Field(f => f.Passport).Type<StringType>();
            descriptor.Field(f => f.BirthDate).Type<DateTimeType>();
        }
    }

    public class AddWagonInputType : InputObjectType<AddWagonInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddWagonInput> descriptor)
        {
            descriptor.Name("AddWagonInput");
            descriptor.Field(f => f.TrainId).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Number).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Type).Name("type").Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Capacity).Type<NonNullType<IntType>>();
        }
    }

    public class UpdateWagonInputType : InputObjectType<UpdateWagonInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateWagonInput> descriptor)
        {
            descriptor.Name("UpdateWagonInput");
            descriptor.Field(f => f.Id).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.TrainId).Type<IntType>();
            descriptor.Field(f => f.Number).Type<IntType>();
            descriptor.Field(f => f.Type).Name("type").Type<StringType>();
            descriptor.Field(f => f.Capacity).Type<IntType>();
        }
    }

    public class AddTicketInputType : InputObjectType<AddTicketInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddTicketInput> descriptor)
        {
            descriptor.Name("AddTicketInput");
            descriptor.Field(f => f.PassengerId).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.TrainId).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.WagonId).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.SeatNumber).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Price).Type<NonNullType<DecimalType>>();
            descriptor.Field(f => f.PurchaseDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(f => f.IsSold).Type<NonNullType<BooleanType>>();
        }
    }

    public class UpdateTicketInputType : InputObjectType<UpdateTicketInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateTicketInput> descriptor)
        {
            descriptor.Name("UpdateTicketInput");
            descriptor.Field(f => f.Id).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.PassengerId).Type<IntType>();
            descriptor.Field(f => f.TrainId).Type<IntType>();
            descriptor.Field(f => f.WagonId).Type<IntType>();
            descriptor.Field(f => f.SeatNumber).Type<IntType>();
            descriptor.Field(f => f.Price).Type<DecimalType>();
            descriptor.Field(f => f.PurchaseDate).Type<DateTimeType>();
            descriptor.Field(f => f.IsSold).Type<BooleanType>();
        }
    }

    public class AddSellerInputType : InputObjectType<AddSellerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddSellerInput> descriptor)
        {
            descriptor.Name("AddSellerInput");
            descriptor.Field(f => f.FullName).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Office).Type<NonNullType<StringType>>();
        }
    }

    public class UpdateSellerInputType : InputObjectType<UpdateSellerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateSellerInput> descriptor)
        {
            descriptor.Name("UpdateSellerInput");
            descriptor.Field(f => f.Id).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.FullName).Type<StringType>();
            descriptor.Field(f => f.Office).Type<StringType>();
        }
    }
}