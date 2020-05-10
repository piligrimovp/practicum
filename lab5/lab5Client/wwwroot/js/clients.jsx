import React from 'react';
import render from 'react-dom';

class CommentBox extends React.Component {
    render() {
        return (
            <div>
                Hello, world! I am a CommentBox.
            </div>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));