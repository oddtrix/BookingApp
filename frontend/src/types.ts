export type Resource = {
    id: Guid;
    name: string;
    type: string;
    capacity: number;
    isActive: boolean;
};

export type BookingStatus = "Pending" | "Confirmed" | "Cancelled";

export type Booking = {
  id: Guid;
  resourceId: number;
  resourceName: string;
  username: string;
  startTime: string;
  endTime: string;
  status: BookingStatus;
  createdAt: string;
};

export type Guid = string & { readonly __brand: unique symbol };