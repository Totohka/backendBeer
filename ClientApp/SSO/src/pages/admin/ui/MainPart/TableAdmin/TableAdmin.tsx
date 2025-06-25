import { Table } from "@chakra-ui/react";
import type { BaseModel } from "./../../../model/BaseModel";

export type TableAdminProps = {
  headers: string[];
  models: BaseModel[];
};

export const TableAdmin = ({ headers, models }: TableAdminProps) => {
  // TODO: мб стоит добавить виртуализацию, если ожидается большое количество данных
  return (
    <Table.ScrollArea borderWidth="1px" rounded="md" height="90vh" width="90vw">
      <Table.Root size="sm" striped stickyHeader showColumnBorder>
        <Table.Header>
          <Table.Row>
            {/* FIXME: лишний фрагмент */}
            <>
              {headers.map((header) => {
                // FIXME: нет ключа, не оч. В базе надо вставить index ручками, чтобы точно было понятно, а сейчас выглядит как будто ты просто забыл его
                return <Table.ColumnHeader>{header}</Table.ColumnHeader>;
              })}
            </>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {models.map((model) => (
            // FIXME: нет ключа, не оч. В базе надо вставить index ручками, чтобы точно было понятно, а сейчас выглядит как будто ты просто забыл его
            <Table.Row>
              <>
                {Object.keys(model).map((key) => {
                  // FIXME: лишний return, не оч тк код более грязный или тип того
                  return (
                    // FIXME: нет ключа, не оч. В базе надо вставить index ручками, чтобы точно было понятно, а сейчас выглядит как будто ты просто забыл его
                    <Table.Cell>
                      {String(model[key as keyof typeof model])}
                    </Table.Cell>
                  );
                })}
              </>
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </Table.ScrollArea>
  );
};
