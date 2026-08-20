import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { bookingsApi, resourcesApi } from "./api";
import type { Guid } from "./types";
  
export const useResources = () =>
  useQuery({
    queryKey: ["resources"],
    queryFn: resourcesApi.getAll,
  });

export const useBookings = (resourceId: Guid) =>
  useQuery({
    queryKey: ["bookings", resourceId],
    queryFn: () => bookingsApi.getAll(resourceId),
  });

export const useCreateBooking = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: bookingsApi.create,
    onSuccess: () => qc.invalidateQueries({ queryKey: ["bookings"] }),
  });
};

export const useCancelBooking = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: bookingsApi.cancel,
    onSuccess: () => qc.invalidateQueries({ queryKey: ["bookings"] }),
  });
};

export const useCreateResource = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: resourcesApi.create,
    onSuccess: () => qc.invalidateQueries({ queryKey: ["resources"] }),
  });
};

export const useUpdateResource = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: ({ id, data }: { id: Guid; data: { name: string; type: string; capacity: number; isActive: boolean } }) =>
      resourcesApi.update(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ["resources"] }),
  });
};

export const useDeleteResource = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: resourcesApi.delete,
    onSuccess: () => qc.invalidateQueries({ queryKey: ["resources"] }),
  });
};