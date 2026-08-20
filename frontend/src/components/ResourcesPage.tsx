import { useState } from "react";
import { useCreateResource, useDeleteResource, useResources, useUpdateResource } from "../hooks";
import type { Guid, Resource, ResourceType } from "../types";

const RESOURCE_TYPES: ResourceType[] = ["Room", "Hall", "Gym"];

export function ResourcesPage() {
  const { data: resources = [], isLoading } = useResources();
  const createMutation = useCreateResource();
  const updateMutation = useUpdateResource();
  const deleteMutation = useDeleteResource();

  const [editingId, setEditingId] = useState<Guid | null>(null);

  const [name, setName] = useState("");
  const [type, setType] = useState<string>("Room");
  const [capacity, setCapacity] = useState<number>(1);
  const [isActive, setIsActive] = useState(true);

  const resetForm = () => {
    setEditingId(null);
    setName("");
    setType("Room");
    setCapacity(1);
    setIsActive(true);
  };

  const handleEditClick = (resource: Resource) => {
    setEditingId(resource.id);
    setName(resource.name);
    setType(resource.type);
    setCapacity(resource.capacity);
    setIsActive(resource.isActive);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (editingId) {
      updateMutation.mutate(
        { id: editingId, data: { name, type, capacity, isActive } },
        { onSuccess: resetForm }
      );
    } else {
      createMutation.mutate(
        { name, type, capacity },
        { onSuccess: resetForm }
      );
    }
  };

  if (isLoading) return <p>Loading resources...</p>;

  return (
    <div className="space-y-8">
      <h1 className="text-2xl font-bold">Resource Management</h1>

      <form onSubmit={handleSubmit} className="flex flex-wrap items-center gap-4">
        <h2 className="w-full text-lg font-semibold">
          {editingId ? "Edit Resource" : "Add New Resource"}
        </h2>

        <input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Resource Name"
          className="border rounded p-2 bg-white"
          required
        />

        <select
          value={type}
          onChange={(e) => setType(e.target.value)}
          className="border rounded p-2 bg-white hover:cursor-pointer"
        >
          {RESOURCE_TYPES.map((t) => (
            <option key={t} value={t}>
              {t}
            </option>
          ))}
        </select>

        <input
          type="number"
          min={1}
          value={capacity}
          onChange={(e) => setCapacity(Number(e.target.value))}
          placeholder="Capacity"
          className="border rounded p-2 w-28 bg-white"
          required
        />

        {editingId && (
          <label className="flex items-center gap-2 text-sm font-medium hover:cursor-pointer">
            <input
              type="checkbox"
              checked={isActive}
              onChange={(e) => setIsActive(e.target.checked)}
              className="h-4 w-4"
            />
            Active
          </label>
        )}

        <div className="flex gap-2">
          <button
            type="submit"
            disabled={createMutation.isPending || updateMutation.isPending}
            className="rounded bg-black px-4 py-2 text-white disabled:opacity-50 hover:cursor-pointer hover:bg-white hover:text-black hover:border"
          >
            {editingId ? "Update" : "Create"}
          </button>

          {editingId && (
            <button
              type="button"
              onClick={resetForm}
              className="rounded border px-4 py-2 hover:bg-gray-200"
            >
              Cancel
            </button>
          )}
        </div>
      </form>

      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="border-b">
              <th className="p-2">Name</th>
              <th className="p-2">Type</th>
              <th className="p-2">Capacity</th>
              <th className="p-2">Status</th>
              <th className="p-2 text-right">Actions</th>
            </tr>
          </thead>
          <tbody>
            {resources.map((res) => (
              <tr key={res.id} className="border-b hover:bg-gray-50">
                <td className="p-2 font-medium">{res.name}</td>
                <td className="p-2">{res.type}</td>
                <td className="p-2">{res.capacity}</td>
                <td className="p-2">
                  <span
                    className={`inline-block px-2 py-0.5 text-xs rounded ${
                      res.isActive ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"
                    }`}
                  >
                    {res.isActive ? "Active" : "Inactive"}
                  </span>
                </td>
                <td className="p-2 text-right space-x-4">
                  <button
                    onClick={() => handleEditClick(res)}
                    className="text-blue-600 hover:underline hover:cursor-pointer"
                  >
                    Edit
                  </button>
                  <button
                    onClick={() => deleteMutation.mutate(res.id)}
                    disabled={deleteMutation.isPending}
                    className="text-red-600 hover:underline disabled:opacity-50 hover:cursor-pointer"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}