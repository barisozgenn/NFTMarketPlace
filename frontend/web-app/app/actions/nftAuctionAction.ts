'use server'

import { NFTAuction, PagedResult } from "@/types";
import { getTokenWorkaround } from "./authAction";

export async function getData(query: string): Promise<PagedResult<NFTAuction>> {
    const res = await fetch(`http://localhost:6001/search/nftauctions${query}`);

    if (!res.ok) throw new Error('Failed to fetch data');

    return res.json();
}


export async function UpdateNFTAuctionTest() {
    const data = {
        //types/index.ts/NFTAuction:indexInCollection
        indexInCollection: Math.floor(Math.random() * 100000) + 1
    }

    const token = await getTokenWorkaround();

    const res = await fetch('http://localhost:6001/nftauctions/put_here_nftauctionId', {
        method: 'PUT',
        headers: {
            'Content-type': 'application/json',
            'Authorization': 'Bearer ' + token?.access_token
        },
        body: JSON.stringify(data)
    })

    if (!res.ok) return {status: res.status, message: res.statusText}

    return res.statusText;
}