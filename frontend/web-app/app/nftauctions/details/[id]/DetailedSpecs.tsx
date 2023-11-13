'use client';

import {NFTAuction} from "@/types";
import {Table} from "flowbite-react";

type Props = {
    nftAuction: NFTAuction
}
export default function DetailedSpecs({nftAuction}: Props) {
    return (
        <Table striped={true}>
            <Table.Body className="divide-y">
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Seller
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.seller}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Name
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.name}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Collection
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.collection}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Artist
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.artist}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Index in the Collection
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.indexInCollection}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Tags
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.tags}
                    </Table.Cell>
                </Table.Row>
                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        Has reserve price?
                    </Table.Cell>
                    <Table.Cell>
                        {nftAuction.reservePrice > 0 ? 'Yes' : 'No'}
                    </Table.Cell>
                </Table.Row>
            </Table.Body>
        </Table>
    );
}