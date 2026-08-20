import { parseToGuid } from "../helper";
import { useBookings, useCancelBooking } from "../hooks";

export function BookingsTable({ resourceId } : { resourceId: string}) {
  const { data: bookings = [], isLoading } = useBookings(parseToGuid(resourceId));
  const cancelMutation = useCancelBooking();

  if (isLoading) return <p>Loading...</p>;

  return (
    <div className="overflow-x-auto">
      <table className="w-full text-left border-collapse">
        <thead>
          <tr className="border-b">
            <th className="p-2">Resource</th>
            <th className="p-2">User</th>
            <th className="p-2">Start Time</th>
            <th className="p-2">End Time</th>
            <th className="p-2">Dutation</th>
            <th className="p-2">Status</th>
            <th className="p-2" />
          </tr>
        </thead>
        <tbody>
          {bookings.map((b) => (
            <tr key={b.id} className="border-b hover:bg-gray-50">
              <td className="p-2">{b.resourceName}</td>
              <td className="p-2">{b.username}</td>
              <td className="p-2">{new Date(b.startTime).toLocaleString()}</td>
              <td className="p-2">{new Date(b.endTime).toLocaleString()}</td>
              <td className="p-2">{new Date(new Date(b.endTime).getTime() - new Date(b.startTime).getTime()).toISOString().substring(11, 19)}</td>
              <td className="p-2">{b.status}</td>
              <td className="p-2">
                {b.status !== "Cancelled" && (
                  <button
                    onClick={() => cancelMutation.mutate(b.id)}
                    disabled={cancelMutation.isPending}
                    className="text-red-600 hover:underline disabled:opacity-50"
                  >
                    Cancel
                  </button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}