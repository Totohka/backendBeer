import { Icon, Popover, Portal } from "@chakra-ui/react"
import { useState, type JSX } from "react";

export type PopoverIconProps = {
    text : string
    icon : JSX.Element
}

export const PopoverIcon = ({ text, icon } : PopoverIconProps) => {
    const [open, setOpen] = useState(false);
    return (
        <Popover.Root open={open} onOpenChange={(e) => setOpen(e.open)}>
            <Popover.Trigger asChild>
                <Icon>{icon}</Icon>
            </Popover.Trigger>
            <Portal>
                <Popover.Positioner>
                <Popover.Content>
                    <Popover.Arrow />
                    <Popover.Body>
                        {text}
                    </Popover.Body>
                </Popover.Content>
                </Popover.Positioner>
            </Portal>
        </Popover.Root>
    )
}

