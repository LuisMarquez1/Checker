import { List, ListItem, ListItemButton, ListItemText } from "@mui/material";
import { NavLink } from "react-router-dom";

function NavigationMenu(){
    return(
        <List>
            <ListItem disablePadding>
                <ListItemButton component={NavLink} to="/" className={({ isActive }) => isActive ? "active" : "" } sx={{ "&.active": { backgroundColor: "#e3f2fd"}}}>
                    <ListItemText primary="Dashboard" />
                </ListItemButton>
            </ListItem>

            <ListItem disablePadding>
                <ListItemButton component={NavLink} to="/operator" className={({ isActive }) => isActive ? "active" : "" } sx={{ "&.active": { backgroundColor: "#e3f2fd"}}}>
                    <ListItemText primary="Operator" />
                </ListItemButton>
            </ListItem>

            <ListItem disablePadding>
                <ListItemButton component={NavLink} to="/specifications" className={({ isActive }) => isActive ? "active" : "" } sx={{ "&.active": { backgroundColor: "#e3f2fd"}}}>
                    <ListItemText primary="Specifications" />
                </ListItemButton>
            </ListItem>

            <ListItem disablePadding>
                <ListItemButton component={NavLink} to="/specifications/create" className={({ isActive }) => isActive ? "active" : "" } sx={{ "&.active": { backgroundColor: "#e3f2fd"}}}>
                    <ListItemText primary="Create Specs" />
                </ListItemButton>
            </ListItem>

            <ListItem disablePadding>
                <ListItemButton component={NavLink} to="/sessions" className={({ isActive }) => isActive ? "active" : "" } sx={{ "&.active": { backgroundColor: "#e3f2fd"}}}>
                    <ListItemText primary="Sessions" />
                </ListItemButton>
            </ListItem>
        </List>
    );
}

export default NavigationMenu;