import { NFTAuction, NFTAuctionFinished } from '@/types'
import Image from 'next/image'
import Link from 'next/link'
import React from 'react'
import { numberWithCommas } from '../lib/numberWithCommas'

type Props = {
    finishedAuction: NFTAuctionFinished
    nftAuction: NFTAuction
}

export default function AuctionFinishedToast({ nftAuction, finishedAuction }: Props) {
    return (
        <Link href={`/nftauctions/details/${nftAuction.id}`} className='flex flex-col items-center'>
            <div className='flex flex-row items-center gap-2'>
                <Image
                    src={nftAuction.contentUrl}
                    alt='image'
                    height={80}
                    width={80}
                    className='rounded-lg w-auto h-auto'
                />
                <div className='flex flex-col'>
                    <span>Auction for {nftAuction.name} {nftAuction.collection} has finished</span>
                    {finishedAuction.itemSold && finishedAuction.price ? (
                        <p>Congrats to {finishedAuction.winner} who has won this auction for 
                            $${numberWithCommas(finishedAuction.price)}</p>
                    ) : (
                        <p>This item did not sell</p>
                    )}
                </div>

            </div>
        </Link>
    )
}