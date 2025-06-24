import { Portal, Select as SelectChakra, type ListCollection } from "@chakra-ui/react"

export type SelectProps = {
    title : string
    placeholder : string
    list : ListCollection<any>
}

export const Select = ({ title, placeholder, list } : SelectProps) => {
  return (
    <SelectChakra.Root collection={list} size="sm" width="320px">
      <SelectChakra.HiddenSelect />
      <SelectChakra.Label>{title}</SelectChakra.Label>
      <SelectChakra.Control>
        <SelectChakra.Trigger>
          <SelectChakra.ValueText placeholder={placeholder} />
        </SelectChakra.Trigger>
        <SelectChakra.IndicatorGroup>
          <SelectChakra.Indicator />
        </SelectChakra.IndicatorGroup>
      </SelectChakra.Control>
      <Portal>
        <SelectChakra.Positioner>
          <SelectChakra.Content>
            {list.items.map((item) => (
              <SelectChakra.Item item={item} key={item.value}>
                {item.label}
                <SelectChakra.ItemIndicator />
              </SelectChakra.Item>
            ))}
          </SelectChakra.Content>
        </SelectChakra.Positioner>
      </Portal>
    </SelectChakra.Root>
  )
}