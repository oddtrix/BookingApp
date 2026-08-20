import { useState } from "react";
import { useCreateBooking, useResources } from "../hooks";
import React from "react";
import { parseToGuid } from "../helper";

export function BookingForm({ resourceId, onResourceChange }: { resourceId: string; onResourceChange: (id: string) => void; }) {
  const { data: resources = [] } = useResources();
  const mutation = useCreateBooking();

  const [userName, setUserName] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  const submit = (e: React.FormEvent) => {
    e.preventDefault();

    mutation.mutate({
      resourceId: parseToGuid(resourceId),
      userName,
      startTime: new Date(startTime).toISOString(),
      endTime: new Date(endTime).toISOString(),
    }, {
      onSuccess: () => {
        onResourceChange("");
        setUserName("");
        setStartTime("");
        setEndTime("");
      },
    });
  };

  return (
    <form onSubmit={submit} className="space-y-4">
      <select
        value={resourceId}
        onChange={e => onResourceChange(e.target.value)}
        className="border rounded p-2 mr-6 hover:cursor-pointer"
        required
      >
        <option value="">Select resource</option>
        {resources.filter(x => x.isActive).map(x => (
          <option key={x.id} value={x.id}>
            {x.name}
          </option>
        ))}
      </select>

      <input
        value={userName}
        onChange={e => setUserName(e.target.value)}
        placeholder="Your name"
        className="border rounded p-2 mr-6"
        required
      />

      <input
        type="datetime-local"
        value={startTime}
        onChange={e => setStartTime(e.target.value)}
        className="border rounded p-2 mr-6 hover:cursor-pointer"
        required
      />

      <input
        type="datetime-local"
        value={endTime}
        onChange={e => setEndTime(e.target.value)}
        className="border rounded p-2 mr-6 hover:cursor-pointer"
        required
      />

      <button
        disabled={mutation.isPending}
        className="rounded bg-black px-4 py-2 text-white disabled:opacity-50 hover:cursor-pointer hover:bg-white hover:text-black hover:border"
      >
        {mutation.isPending ? "Booking..." : "Book"}
      </button>

      {mutation.isError && (
        <p className="text-red-600 text-sm">{mutation.error.message}</p>
      )}
    </form>
  );
}