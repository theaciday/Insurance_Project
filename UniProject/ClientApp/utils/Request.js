const request = async (url, params = {}, body) => {
    const urll = new URL(`https://localhost:5001/api/${url}`)
    if (params.queryParams) {
        Object.keys(params.queryParams).forEach(key => urll.searchParams.append(key, params.queryParams[key]))
    }
    const token = localStorage.getItem('token')
    const response = await fetch(urll, {
        headers: {
            ...!params.contentType && { 'Content-type': 'application/json' },
            ...(token && { 'Authorization': token })
        },
        ...params.method && { method: params.method },
        ...body && { body: params.contentType ? body : JSON.stringify(body), },
    })

    //'Content-type': 'application/json',

    const text = await response.text();

    const data = text && JSON.parse(text)

    if (!response.ok) {
        if (response.status === 401) {
            // auto logout if 401 response returned from api
            //logout();
            //location.reload(true);
        }

        const error = (data && data.message) || response.statusText;
        return Promise.reject(error);
    }
    return data;
}
export default request;