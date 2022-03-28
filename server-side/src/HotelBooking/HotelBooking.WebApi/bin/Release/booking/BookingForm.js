import React from 'react';

function Form() {
    return (

        <div className="container col-5 col-xs-10 
        col-sm-9 col-md-7 col-lg-5">
            <h2>Book a room</h2>
            <div className="border border-info rounded">
                <form method="post">
                    <input type="text"/>
                </form>
            </div>
        </div>
    );
}

export class BookingForm extends React.Component {

    render() {
        return (
            <Form />
        );
    }
}