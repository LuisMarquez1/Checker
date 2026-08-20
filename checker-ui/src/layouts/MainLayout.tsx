import { AppBar, Box, CssBaseline, Drawer, Toolbar, Typography } from "@mui/material";
import NavigationMenu from "../components/NavigationMenu";

const drawerWidth = 240;

interface MainLayoutProps{
    children: React.ReactNode;
}

function MainLayout({ children } : MainLayoutProps) {
    return (
        <Box sx={{ display : "flex" }}>
            <CssBaseline />

            <AppBar position="fixed" sx={{ zIndex: 1201 }}>
                <Toolbar>
                    <Typography variant="h6" noWrap>
                        Checker
                    </Typography>
                </Toolbar>
            </AppBar>

            <Drawer variant="permanent" sx={{ Width: drawerWidth, flexShrink: 0, "& .MuiDrawer-paper": { width: drawerWidth, boxSizing: "border-box", }, }}>
                <Toolbar />
                <Box sx={{ overflow: "auto" }}>
                    <NavigationMenu />
                </Box>

            </Drawer>
            <Box component="main" sx={{ flexGrow: 1, p:3, ml: `${drawerWidth}px` }}>
                <Toolbar />
                { children }
            </Box>
        </Box>
    );
}

export default MainLayout;