namespace Domain.Errors
{
    public static class ErrorMessages
    {
        public const string EndTimeMustBeGreaterThanStartTime = "EndTime must be greater than StartTime.";
        public const string ResourceNotFound = "Resource not found.";
        public const string ResourceIsInactive = "Resource is inactive.";
        public const string SelectedSlotIsBooked = "The selected time slot is already booked.";
        public const string UsernameIsRequired = "Username is required.";

        public const string InvalidResource = "Invalid resource. Please check your data again.";
    }
}
