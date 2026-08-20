import type { Booking, Guid, Resource } from "./types";

const request = async <T>(url: string, options?: RequestInit): Promise<T> => {
    const res = await fetch(`https://localhost:7273/api${url}`, {
      headers: { "Content-Type": "application/json" },
      ...options,
    });
  
    if (!res.ok) {
      const body = await res.json().catch(() => null);
      throw new Error(body?.message ?? "Request failed");
    }
  
    if (res.status === 204) return undefined as T;
    return res.json();
};

export const resourcesApi = {
    getAll: () => request<Resource[]>("/resources"),
  
    create: (data: {
      name: string;
      type: string;
      capacity: number;
    }) =>
      request<Resource>("/resources", {
        method: "POST",
        body: JSON.stringify(data),
      }),
  
    update: (id: Guid, data: {
      name: string;
      type: string;
      capacity: number;
      isActive: boolean;
    }) =>
      request<void>(`/resources/${id}`, {
        method: "PUT",
        body: JSON.stringify(data),
      }),
  
    delete: (id: Guid) =>
      request<void>(`/resources/${id}`, {
        method: "DELETE",
      }),
};

export const bookingsApi = {
    getAll: (resourceId?: Guid) =>
      request<Booking[]>(
        `/bookings${resourceId ? `?resourceId=${resourceId}` : ""}`
      ),
  
    create: (data: {
      resourceId: Guid;
      userName: string;
      startTime: string;
      endTime: string;
    }) =>
      request<Booking>("/bookings", {
        method: "POST",
        body: JSON.stringify(data),
      }),
  
    cancel: (id: Guid) =>
      request<void>(`/bookings/${id}/cancel`, {
        method: "POST",
      }),
};