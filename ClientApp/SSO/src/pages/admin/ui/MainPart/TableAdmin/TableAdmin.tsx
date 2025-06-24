import { Table } from "@chakra-ui/react";
import type { BaseModel } from "./../../../model/BaseModel";

export type TableAdminProps = {
    headers : string[]
    models : BaseModel[]
}

export const TableAdmin = ({ headers, models } : TableAdminProps) => {
    return (
        <Table.ScrollArea borderWidth="1px" rounded="md" height="90vh" width="90vw">
            <Table.Root size="sm" striped stickyHeader showColumnBorder>
                <Table.Header>
                    <Table.Row>
                        <>
                            {
                                headers.map(header => { 
                                    return <Table.ColumnHeader>{header}</Table.ColumnHeader>
                                })
                            }
                        </>
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {models.map((model) => (
                        <Table.Row>
                            <>
                                {
                                    Object.keys(model).map(key => {
                                        return <Table.Cell>{String(model[key as keyof typeof model])}</Table.Cell>
                                    })
                                }
                            </>
                        </Table.Row>
                    ))}
                </Table.Body>
            </Table.Root>
        </Table.ScrollArea>
    )
}