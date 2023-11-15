'use client'

type Props = {
    nftAuctionId: string;
    highestBid: number;
}

import { placeBidForNFTAuction } from '@/app/actions/nftAuctionAction';
import { numberWithCommas } from '@/app/lib/numberWithCommas';
import { useBidStore } from '@/hooks/useBidStore';
import React from 'react'
import { FieldValues, useForm } from 'react-hook-form';
import { toast } from 'react-hot-toast';

export default function BidForm({ nftAuctionId, highestBid }: Props) {
    const {register, handleSubmit, reset, formState: {errors}} = useForm();
    const addBid = useBidStore(state => state.addBid);

    function onSubmit(data: FieldValues) {
        if (data.amount <= highestBid) {
            reset();
            return toast.error('Bid must be at least $' + numberWithCommas(highestBid + 1))
        }
        placeBidForNFTAuction(nftAuctionId, +data.amount).then(bid => {
            if (bid.error) throw bid.error;
            addBid(bid);
            reset();
        }).catch(err => toast.error(err.message));
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className='flex items-center border-2 rounded-lg py-2'>
            <input 
                type="number" 
                {...register('amount')}
                className='input-custom-baris text-sm text-gray-600'
                placeholder={`Enter your bid (minimum bid is $${numberWithCommas(highestBid + 1)})`}
            />
        </form>
    )
}