import { createStore, applyMiddleware, compose } from 'redux';
import thunkMiddleware from 'redux-thunk';
import rootReducer from './_reducers';

export const store = createStore(
    rootReducer,
    compose(
        applyMiddleware(
            thunkMiddleware,
        ),
        window.__REDUX_DEVTOOLS_EXTENSION__ && process.env.NODE_ENV !== 'production'
            ? window.__REDUX_DEVTOOLS_EXTENSION__()
            : f => f)
);
