import { createRootRoute, createRoute, createRouter, Link, Outlet } from "@tanstack/react-router";
import { BookingForm } from "./components/BookingForm";
import { BookingsTable } from "./components/BookingsTable";
import { useState } from "react";
  
const root = createRootRoute({
    component: () => (
        <main className="mx-auto max-w-6xl p-6">
            <nav className="mb-6 flex gap-4">
                <Link to="/" className="font-bold hover:underline">
                    Bookings
                </Link>
            </nav>
            <Outlet />
        </main>
    ),
});

const indexRoute = createRoute({
    getParentRoute: () => root,
    path: "/",
    component: () => {
        const [selectedResource, setSelectedResource] = useState<string>("");

        return (
            <div>
                <h1 className="mb-6 text-2xl font-bold">Resource Booking</h1>
                <BookingForm resourceId={selectedResource} onResourceChange={setSelectedResource}/>
                <div className="mt-10">
                    <BookingsTable resourceId={selectedResource}/>
                </div>
            </div>
        );
    }
});

const routeTree = root.addChildren([indexRoute]);

export const router = createRouter({ routeTree });