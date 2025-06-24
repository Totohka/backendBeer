import { Button, Icon, Input, Stat } from "@chakra-ui/react"
import { useState } from "react"
import { FaPenSquare } from "react-icons/fa"

export type StatUpdateProps = {
    label : string
    value : string
}

export const StatUpdate = ({ label, value } : StatUpdateProps) => {
    const [val, setVal] = useState(value);
    const [update, setUpdate] = useState(false);

    function onSave() {
        setUpdate(false);
    }

    return (
        <>
            <Stat.Label>
                {label} 
                <Icon>
                    <FaPenSquare onClick={() => setUpdate(!update)}/>
                </Icon>
            </Stat.Label>
            {
                update ? 
                <> 
                    <Input value={val} onChange={(e) => setVal(e.target.value)}></Input>
                    <Button onClick={() => {onSave();}}>Сохранить</Button>
                </>
                : 
                <Stat.ValueText>{val}</Stat.ValueText>
            }
        </>
    )
}