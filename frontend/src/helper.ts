import type { Guid } from "./types";

export function parseToGuid(input: string): Guid {
    return input as Guid;
}