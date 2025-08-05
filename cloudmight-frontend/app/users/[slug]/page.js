export default function UserPage({params}){
    return (
        <main>
            <h1>Username: {params.slug}</h1>
        </main>
    );
}