
        'use server'

        import { Bid, NFTAuction, PagedResult } from "@/types";
        import { getTokenWorkaround } from "./authAction";
        import { fetchWrapper } from "@/app/lib/fetchWrapper";
        import { FieldValues } from "react-hook-form";
        import { revalidatePath } from "next/cache";

        export async function getData(query: string): Promise<PagedResult<NFTAuction>> {
            return await fetchWrapper.get(`search/nftAuctions${query}`)
        }

        export async function updateAuctionTest() {
            const data = {
                //types/index.ts/NFTAuction:indexInCollection
                mileage: Math.floor(Math.random() * 100000) + 1
            }

            return await fetchWrapper.put('nftAuctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', data);
        }

        export async function createAuction(data: FieldValues) {
            return await fetchWrapper.post('nftAuctions', data);
        }

        export async function getDetailedViewData(id: string): Promise<NFTAuction> {
            return await fetchWrapper.get(`nftAuctions/${id}`);
        }

        export async function updateAuction(data: FieldValues, id: string) {
            const res = await fetchWrapper.put(`nftAuctions/${id}`, data);
            revalidatePath(`/nftAuctions/${id}`);
            return res;
        }

        export async function deleteAuction(id: string) {
            return await fetchWrapper.del(`nftAuctions/${id}`);
        }

        export async function getBidsForNFTAuction(id: string): Promise<Bid[]> {
            return await fetchWrapper.get(`bids/${id}`);
        }

        export async function placeBidForNFTAuction(nftAuctionId: string, price: number) {
            return await fetchWrapper.post(`bids?nftAuctionId=${nftAuctionId}&price=${price}`, {})
        }