import React from 'react'
import CountdownTimer from './CountdownTimer'
import NFTImage from './NFTImage'
import { NFTAuction } from '@/types'

type Props = {
    nftauction: NFTAuction
}
export default function NFTAuctionCard({ nftauction }: Props) {
    return (
        <a href='#' className='group'>
            <div className='w-full bg-gray-200 aspect-w-16 aspect-h-10 rounded-lg overflow-hidden'>
                <div>
                    <NFTImage contentUrl={nftauction.contentUrl} />
                    <div className='absolute bottom-2 left-2'>
                        <CountdownTimer auctionEnd={nftauction.nftAuctionEndAt} />
                    </div>
                </div>
            </div>
            <div className='flex justify-between items-center mt-4'>
                <h3 className='text-gray-700'>{nftauction.name} {nftauction.collection}</h3>
                <p className='font-semibold text-sm'>{nftauction.artist}</p>
            </div>

        </a>
    )
}
