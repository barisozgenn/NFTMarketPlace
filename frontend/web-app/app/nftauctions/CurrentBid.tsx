import React from 'react'

type Props = {
    price?: number
    reservePrice: number
}

export default function CurrentBid({ price, reservePrice }: Props) {
    const text = price ? '$' + price : 'No bids';
    const color = price ? price > reservePrice ? 'bg-green-600' : 'bg-amber-600' : 'bg-red-600'

    return (
        <div className={`
            border-2 border-white text-white py-1 px-2 rounded-lg flex
            justify-center ${color}
        `}>
            {text}
        </div>
    )
}