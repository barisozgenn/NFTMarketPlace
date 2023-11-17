'use client'

import { Button, TextInput } from 'flowbite-react';
import React, { useEffect } from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import Input from '../components/Input';
import DateInput from '../components/DateInput';
import { createAuction, updateAuction } from '../actions/nftAuctionAction';
import { usePathname, useRouter } from 'next/navigation';
import { toast } from 'react-hot-toast';
import { NFTAuction } from '@/types';

type Props = {
    nftAuction?: NFTAuction
}

export default function NFTAuctionForm({ nftAuction }: Props) {
    const router = useRouter();
    const pathname = usePathname();
    const { control, handleSubmit, setFocus, reset,
        formState: { isSubmitting, isValid } } = useForm({
            mode: 'onTouched'
        });

    useEffect(() => {
        if (nftAuction) {
            const { name, collection, artist, indexInCollection, tags } = nftAuction;
            reset({ name, collection, artist, indexInCollection, tags });
        }
        setFocus('name');
    }, [setFocus, reset, nftAuction]) //we are adding here dependencies

    async function onSubmit(data: FieldValues) {
        try {
            let id = '';
            let res;
            if (pathname === '/nftauctions/create') {
                res = await createAuction(data);
                id = res.id;
            } else {
                if (nftAuction) {
                    res = await updateAuction(data, nftAuction.id);
                    id = nftAuction.id;
                }
            }
            if (res.error) {
                throw res.error;
            }
            router.push(`/nftauctions/details/${id}`)
        } catch (error: any) {
            toast.error(error.status + ' ' + error.message)
        }
    }

    return (
        <form className='flex flex-col mt-3' onSubmit={handleSubmit(onSubmit)}>
            <Input label='Name' name='name' control={control}
                rules={{ required: 'Name is required' }} />
            <Input label='Collection' name='collection' control={control}
                rules={{ required: 'Collection is required' }} />
            <Input label='Artist' name='artist' control={control}
                rules={{ required: 'Artist is required' }} />

            <div className='grid grid-cols-2 gap-3'>
                <Input label='NFT index in the collection' name='indexInCollection' control={control} type='number'
                    rules={{ required: 'NFT index in the collection is required' }} />
                <Input label='Tags' name='tags' control={control}
                    rules={{ required: 'Tags is required' }} />
            </div>

            {pathname === '/nftauctions/create' &&
            <>
                <Input label='Content URL(image)' name='contentUrl' control={control}
                    rules={{ required: 'Content URL is required' }} />

                <div className='grid grid-cols-2 gap-3'>
                    <Input label='Reserve Price (enter 0 if no reserve)'
                        name='reservePrice' control={control} type='number'
                        rules={{ required: 'Reserve price is required' }} />
                    <DateInput
                        label='NFT Auction end date/time'
                        name='nftAuctionEndAt'
                        control={control}
                        dateFormat='dd MMMM yyyy h:mm a'
                        showTimeSelect
                        rules={{ required: 'NFT Auction end date is required' }} />
                </div>
            </>}


            <div className='flex justify-between'>
                <Button outline color='gray'>Cancel</Button>
                <Button
                    isProcessing={isSubmitting}
                    disabled={!isValid}
                    type='submit'
                    outline color='success'>Submit</Button>
            </div>
        </form>
    )
}