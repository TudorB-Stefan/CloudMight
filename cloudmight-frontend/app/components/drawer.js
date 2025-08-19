import Link from "next/link";

export default function Drawer() {
    const closeDrawer = () => {
        const drawerToggle = document.getElementById("my-drawer");
        if (drawerToggle) drawerToggle.checked = false;
    };
    return (
        <>
            <input id="my-drawer" type="checkbox" className="drawer-toggle hidden" />
            <input id="main-drawer" type="checkbox" className="drawer-toggle hidden" />
            <div className="drawer-side fixed top-0 left-0 z-[9999] ">
                <label htmlFor="my-drawer" aria-label="close sidebar" className="drawer-overlay"></label>
                <ul className="menu bg-zinc-200/70 text-xl text-zinc-700 min-h-full w-80 p-4 pt-8">
                    <Link href={"/"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80 hover:rounded-t-xl" onClick={closeDrawer}>Home</Link>
                    <Link href={"/downloads"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80" onClick={closeDrawer}>Downloads</Link>
                    <Link href={"/contact"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80" onClick={closeDrawer}>Contact</Link>
                    <Link href={"/about"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80" onClick={closeDrawer}>About Us</Link>
                    <Link href={"/terms-of-use"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80" onClick={closeDrawer}>Terms of use</Link>
                    <Link href={"/privacy-policy"} className="border-b border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80" onClick={closeDrawer}>Privacy policy</Link>
                    <Link href={"/cookie-policy"} className="border-zinc-700 h-10 flex items-center hover:bg-zinc-300/80 hover:rounded-b-xl" onClick={closeDrawer}>Cookie policy</Link>
                </ul>
            </div>
        </>    
    );
}