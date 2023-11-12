'use server'

import { NFTAuction, PagedResult } from "@/types";

export async function getData(query: string): Promise<PagedResult<NFTAuction>> {
    const res = await fetch(`http://localhost:6001/search/nftauctions${query}`);

    if (!res.ok) throw new Error('Failed to fetch data');

    return res.json();
}