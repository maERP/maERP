﻿using maERP.Application.Dtos.CustomerAddress;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailResponse
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public List<CustomerAddressListDto> CustomerAddresses { get; set; } = new List<CustomerAddressListDto>();
    public DateTime DateEnrollment { get; set; }
}