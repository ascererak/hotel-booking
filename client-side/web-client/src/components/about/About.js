import React from 'react';

function Paragraph (props) {
    return (
        <div>
            <h6>{props.header}</h6>
            <p>{props.text}</p>
        </div>
    );
}

const About = (props) => {
    // Paragraph 1
    var header1 = "The standard Lorem Ipsum passage, used since the 1500s";
    var text1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " + 
    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut" + 
    " enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi" + 
    " ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehend" + 
    "erit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Exc" + 
    "epteur sint occaecat cupidatat non proident, sunt in culpa qui officia" + 
    " deserunt mollit anim id est laborum."

    // Paragrap 2
    var header2 = "Section 1.10.32 of \"de Finibus Bonorum et Malorum\"," + 
    " written by Cicero in 45 BC";
    var text2 = "Sed ut perspiciatis unde omnis iste natus error sit " + 
    "voluptatem accusantium doloremque laudantium, totam rem aperiam, " + 
    "eaque ipsa quae ab illo inventore veritatis et quasi architecto " + 
    "beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia" + 
    " voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur " + 
    "magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro" + 
    " quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur," + 
    " adipisci velit, sed quia non numquam eius modi tempora incidunt ut" + 
    " labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad " + 
    "minima veniam, quis nostrum exercitationem ullam corporis suscipit" + 
    " laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem " + 
    "vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil" + 
    " molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas" + 
    " nulla pariatur?"
    
    return (
        <div>
            <h2>About Us</h2>
            <Paragraph header={header1} text={text1}/>
            <Paragraph header={header2} text={text2}/>
        </div>
    );
}

export default About;